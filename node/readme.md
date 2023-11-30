This code was last tested using Node 16.18.0.

To run the code, use the command `node xortag.js` in the current directory. Node must be installed and in the path for this to work.

If you don't have Node installed but have Docker installed, you can run the code with the following command: `docker run --rm -it -w /app -v ${PWD}:/app node:16.18.0-alpine node xortag.js`.