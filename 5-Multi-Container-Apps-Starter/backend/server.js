const express = require('express');
const mongoose = require('mongoose');
const cors = require('cors');
const fetch = require('node-fetch');
const app = express();

const FavoriteMovie = require('./models/favoriteMovie');

app.use(express.json());
app.use(cors());

app.get('/', async (req, res) => {
  const url = 'https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=sv-SE&page=1&sort_by=popularity.desc';
  const options = {
    method: 'GET',
    headers: {
      accept: 'application/json',
      authorization: 'bearer <Ange token som ni hittar i ert konto på themoviedb.org>',
    },
  };

  const response = await fetch(url, options);

  if (response.status === 200) {
    const result = await response.json();
    res.status(200).json({ status: 'Success', page: result.page, data: result.results });
  } else {
    res.status(404).json({ success: false, message: "Couldn't find any movie" });
  }
});

app.get('/favorites', async (req, res) => {
  const favorites = await FavoriteMovie.find();
  res.status(200).json({ success: true, data: favorites });
});

app.post('/favorites', async (req, res) => {
  const title = req.body.title;
  const id = req.body.id;

  const favorite = new FavoriteMovie({
    title,
    id,
  });

  try {
    await favorite.save();
    res.status(201).json({ success: true, message: 'Favorite movie saved' });
  } catch (error) {
    return res.status(500).json({ success: false, message: error.message });
  }
});

mongoose
  .connect('mongodb://localhost:27017/movies')
  .then(() => {
    console.log('MongoDB is connected');
    app.listen(80);
  })
  .catch((err) => console.error(err));
