This code was last tested using Go 1.19.

To run the code, use the command `go run . ` in the current directory. Go must be installed and in the path for this to work. To compile the code, use the command `go build .`.

If you don't have Go installed but have Docker installed, you can run the code with the following command: `docker run --rm -it -w /app -v ${PWD}:/app golang:1.19-alpine go run .`.