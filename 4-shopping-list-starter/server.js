const fs = require("fs").promises;
const path = require("path");
const express = require("express");
const bodyParser = require("body-parser");

const fileName = "groceries.txt";

const app = express();

app.use(express.static("public"));
app.use(bodyParser.urlencoded({ extended: false }));

// "app.get"= start endpoint - som hämtar index-filen från pages-mappen (så man gör i node)
// detta är en serverbaserad webapp som hämtar en fil från en mapp inte en api
// använder sendFile för att skicka data till klienten
app.get("/", (req, res) => {
  const filePath = path.join(__dirname, "pages", "index.html");
  res.sendFile(filePath);
});

// app.get("/groceries",... hämtar filen groceries.txt från mappen groceries
// använder sendFile för att skicka data till klienten

app.get("/groceries", (req, res) => {
  const filePath = path.join(__dirname, "groceries", fileName);
  res.sendFile(filePath);
});

// app.post("/add",... lägger till en vara i filen groceries.txt
// + "\n" lägger till en radbrytning efter varje vara
app.post("/add", (req, res) => {
  const supply = req.body.grocery + "\n";

  const filePath = path.join(__dirname, "groceries", fileName);

  // appendFile lägger till en vara i filen groceries.txt
  // den tar in det iva req.body.grocery och lägger till det i filen
  // utf8 är en textkodning som används för att representera Unicode
  // som kan hantera alla tecken i alla språk
  // redirectar tillbaka till startsidan
  fs.appendFile(filePath, supply, "utf8")
    .then(() => res.redirect("/"))
    .catch((err) => {
      console.error(err);
    });
});

// lyssnar på port 80
app.listen(80);
// Raden nedan hör ihop med varianten i DockerfileLEKTION
//app.listen(process.env.PORT);
