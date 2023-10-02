const { Schema, model } = require('mongoose');

const favoriteMovieSchema = new Schema({
  title: String,
  id: Number,
});

const FavoriteMovie = model('FavoriteMovie', favoriteMovieSchema);

module.exports = FavoriteMovie;
