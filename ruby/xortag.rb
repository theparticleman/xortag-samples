require 'net/http'
require 'json'
require 'uri'

class XorTag
	
	def move
		register 
		
		puts "You successfully registered!"
		puts "Your player name is #{@world["name"]} and your id is #{@world["id"]}"
		
		while true do
			case rand(4)
			when 0
				move_up 
			when 1
				move_right
			when 2
				move_down
			when 3
				move_left
			end
		end
	end

	def move_up 
		uri = URI.parse(@baseUrl + "/moveup/#{@world["id"]}")
		@world = send uri
	end
	
	def move_right
		uri = URI.parse(@baseUrl + "/moveright/#{@world["id"]}")
		@world = send uri
	end
	
	def move_down
		uri = URI.parse(@baseUrl + "/movedown/#{@world["id"]}")
		@world = send uri
	end
	
	def move_left
		uri = URI.parse(@baseUrl + "/moveleft/#{@world["id"]}")
		@world = send uri
	end

	def register 
		uri = URI.parse(@baseUrl + '/register')
		@world = send uri
	end

	def send uri
		sleep(1) #Requests more frequent than once per second will fail.
		response = Net::HTTP.get(uri)
		return JSON.parse(response)
	end
	
	def initialize
		@baseUrl = 'https://xortag.azurewebsites.net'
	end

end

player = XorTag.new
player.move