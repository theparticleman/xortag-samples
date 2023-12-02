<?php

echo 'Hello world';

class XorTag {
    private $baseUrl = 'https://xortag.azurewebsites.net/';
    private $world;

    function register() {
        $result = file_get_contents($this->baseUrl . 'register');
        $deserialized = json_decode($result);
        $this->world = $deserialized;
        echo 'You have successfully registered!' . PHP_EOL;
        echo 'Your player name is ' . $this->world->name . ' and your id is ' . $this->world->id . PHP_EOL;
        sleep(1);
    }

    function moveUp() {
        $this->world = json_decode(file_get_contents($this->baseUrl . 'moveup/' . $this->world->id));
    }

    function moveDown() {
        $this->world = json_decode(file_get_contents($this->baseUrl . 'movedown/' . $this->world->id));
    }

    function moveLeft() {
        $this->world = json_decode(file_get_contents($this->baseUrl . 'moveleft/' . $this->world->id));
    }

    function moveRight() {
        $this->world = json_decode(file_get_contents($this->baseUrl . 'moveright/' . $this->world->id));
    }

    function look() {
        $this->world = json_decode(file_get_contents($this->baseUrl . 'look/' . $this->world->id));
    }

    function play() {
        $this->register();
        while(true) {
            switch (rand(1, 4)) {
                case 1:
                    $this->moveUp();
                    break;
                case 2:
                    $this->moveDown();
                    break;
                case 3:
                    $this->moveLeft();
                    break;
                case 4:
                    $this->moveRight();
                    break;
            }
            sleep(1);
        }
    }
}

$player = new XorTag();
$player->play();