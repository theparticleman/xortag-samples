var https = require('https');
var baseUrl = 'https://xortag.azurewebsites.net/';

var move = function(){
	switch(Math.floor((Math.random()*4))){
		case 0:
			moveUp();
		break;
		case 1:
			moveDown();
		break;
		case 2:
			moveLeft();
		break;
		case 3:
			moveRight();
		break;
	}
};

var world;

var moveUp = function(){
	request("moveup/" + world.id, move);
};

var moveDown = function(){
	request("movedown/" + world.id, move);
};

var moveLeft = function(){
	request("moveleft/" + world.id, move);
};

var moveRight = function(){
	request("moveright/" + world.id, move);
};

var look = function(){
	request("look/" + world.id, move);
};

var register = function(){
	request("register", function(){
		console.log("You successfully registered");
		console.log("Your player name is " + world.name + " and your id is " + world.id);
		move();
	});
};

var getUpdate = function(onResult) {
	return function(response){
			var str = '';
		response.on('data', function(chunk){
			str += chunk
		});
		response.on('end', function(){
			world = JSON.parse(str);
			if (onResult) onResult();
		});
	};
};

var request = function(url, onResult){
	setTimeout(function(){
		url = baseUrl + url;
		var req = https.get(url, getUpdate(onResult));
		req.end();
	}, 1000); //Requests more frequent that once per second will fail.
};

process.on('SIGINT', () => {
	process.exit();
});

register();