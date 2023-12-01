package main

import (
	"encoding/json"
	"fmt"
	"io"
	"net/http"
	"os"
	"time"
	"math/rand"
)

const baseUrl = "https://xortag.azurewebsites.net/"
var world map[string]interface{} = nil

func main() {
	register()

	for true {
		switch rand.Intn(4) {
		case 0:
			moveUp()
		case 1:
			moveDown()
		case 2:
			moveLeft()
		case 3:
			moveRight()
		}
		time.Sleep(1 * time.Second)
	}
}

func register() {
	world = send(baseUrl + "register")
	fmt.Println("You successfully registered")
	fmt.Println("Your player name is " + world["name"].(string) + " and your id is " +  fmt.Sprintf("%g", world["id"].(float64)))
	time.Sleep(1 * time.Second)
}

func moveUp() {
	world = send(baseUrl + "moveup/" + fmt.Sprintf("%g", world["id"].(float64)))
}

func moveDown() {
	world = send(baseUrl + "movedown/" + fmt.Sprintf("%g", world["id"].(float64)))
}

func moveLeft() {
	world = send(baseUrl + "moveleft/" + fmt.Sprintf("%g", world["id"].(float64)))
}

func moveRight() {
	world = send(baseUrl + "moveright/" + fmt.Sprintf("%g", world["id"].(float64)))
}

func send(url string) map[string]interface{} {
	req, err := http.NewRequest("GET", url, nil)
	if err != nil {
		os.Exit(1)
	}
	
	client := &http.Client{}
	response, err := client.Do(req)
	if err != nil {
		os.Exit(2)
	}

	defer response.Body.Close()

	b, err := io.ReadAll(response.Body)
	if err != nil {
		fmt.Println(err)
		os.Exit(3)
	}

	result := make(map[string]interface{})
	json.Unmarshal([]byte(b), &result)
	return result
}