const fs = require('fs').promises;
const path = require('path');
const express = require('express');
const bodyParser = require('body-parser');

const fileName = 'groceries.txt';

const app = express();

app.use(express.static('public'));
app.use(bodyParser.urlencoded({ extended: false }));

app.get('/', (req, res) => {
  const filePath = path.join(__dirname, 'pages', 'index.html');
  res.sendFile(filePath);
});

app.get('/groceries', (req, res) => {
  const filePath = path.join(__dirname, 'groceries', fileName);
  res.sendFile(filePath);
});

app.post('/add', (req, res) => {
  const supply = req.body.grocery + '\n';

  const filePath = path.join(__dirname, 'groceries', fileName);

  fs.appendFile(filePath, supply, 'utf8')
    .then(() => res.redirect('/'))
    .catch((err) => {
      console.error(err);
    });
});

app.listen(80);
