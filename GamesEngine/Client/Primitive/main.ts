import "./node_modules/bootstrap/dist/js/bootstrap.bundle.min.js";

const canvas = document.getElementById('myCanvas') as HTMLCanvasElement | null;
if (canvas) {
  const context = canvas.getContext('2d');
  
    let x = 50; // Starting x position
    let y = 50; // Starting y position
    const size = 50; // Size of the square
    function drawSquare() {
        // Clear the previous frame
        if (context && canvas) {
            context.clearRect(0, 0, canvas.width, canvas.height);

            context.beginPath();
            context.rect(x, y, size, size);
            context.fillStyle = "#FF0000";
            context.fill();
            context.closePath();

            x += 1;
            y += 1;
        }
    }

    function animate() {
    drawSquare();
    requestAnimationFrame(animate); // Call animate again for the next frame
    }

    animate();
  }

