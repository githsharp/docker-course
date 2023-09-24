import express from 'express';

const app = express();

// Create a dynamic variable...
let pageTitle = 'Välkommen till Oss';

app.use(express.static('public'));

app.get('/', (req, res) => {
  res.send(`
    <!DOCTYPE html>
      <html lang="en">
        <head>
          <meta charset="UTF-8" />
          <meta http-equiv="X-UA-Compatible" content="IE=edge" />
          <meta name="viewport" content="width=device-width, initial-scale=1.0" />
          <script src="https://kit.fontawesome.com/55174c58a9.js" crossorigin="anonymous"></script>
          <link rel="stylesheet" href="styles.css" />
          <link rel="stylesheet" href="utilities.css" />
          <title>CarDealer-App</title>
        </head>
        <body>
          <nav id="navbar">
            <h1 class="logo">
              <span class="text-primary"><i class="fa-solid fa-car"></i>Westcoast </span>Cars
            </h1>
            <ul>
              <li><a href="#">Start</a></li>
              <li><a href="#">Våra bilar</a></li>
              <li><a href="#">Service</a></li>
              <li><a href="#">Om Oss</a></li>
            </ul>
          </nav>
          <main>
            <h1 class="page-title">
              ${pageTitle}
            </h1>
          </main>
        </body>
      </html>
  `);
});

app.listen(80, console.log('Server is running'));
