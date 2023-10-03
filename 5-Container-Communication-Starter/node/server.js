const express = require("express");
const mongoose = require("mongoose");
const fetch = require("node-fetch");
const app = express();

const FavoriteMovie = require("./models/favoriteMovie");

app.use(express.json());

app.get("/", async (req, res) => {
  const url =
    "https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=sv-SE&page=1&sort_by=popularity.desc";
  const options = {
    method: "GET",
    headers: {
      accept: "application/json",
      authorization:
        // Min bearer token
        "bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI2ODVlYjJiZGQwMGVkZDViMzkxYzZkZjY3YTAxM2JkMSIsInN1YiI6IjY1MWJkMDE0ZWE4NGM3MDE0ZWZkMzU5NSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.n245vNkCcrKsmiYr4qCkiRAd6aSSgRHHWDfi6OxdwTI",
      // MGs bearer:
      //"bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJiNmRiMTA4YzY1Njc0M2I1MGZlMzlhZmNjM2JmZmNjNyIsInN1YiI6IjY0MzU5ZjJmZTYzNTcxMDBmMjdhMjNhYSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.E6xnG0gItQtEi9iakurypuc7gwg7IiVK_A3zH3TqBlo",
    },
  };

  const response = await fetch(url, options);

  if (response.status === 200) {
    const result = await response.json();
    res
      .status(200)
      .json({ status: "Success", page: result.page, data: result.results });
  } else {
    res
      .status(404)
      .json({ success: false, message: "Couldn't find any movie" });
  }
});

app.get("/favorites", async (req, res) => {
  const favorites = await FavoriteMovie.find();
  res.status(200).json({ success: true, data: favorites });
});

app.post("/favorites", async (req, res) => {
  const title = req.body.title;
  const id = req.body.id;

  const favorite = new FavoriteMovie({
    title,
    id,
  });

  try {
    await favorite.save();
    res.status(201).json({ success: true, message: "Favorite movie saved" });
  } catch (error) {
    return res.status(500).json({ success: false, message: error.message });
  }
});

//app.listen(80);

// MG provade en kombo (av #2 och #3 nedan)
mongoose
  //.connect("mongodb://127.0.0.1:27017/movies")
  .connect("mongodb://localhost:27017/movies")
  .then(app.listen(80))
  .catch((err) => console.error(err));
//.connect("mongodb://localhost:27017/movies")
//.connect("mongodb://host.docker.internal:27017/movies")
//.connect("mongodb://172.17.0.3:27017/movies")
//.connect("mongodb://loal:27017/movies")
//.then(() => {
//  console.log("Mongodb connected and the server is up and running");
// app.listen(80);
//})
//.catch((err) => console.error(err));

// De 4 raderna nedanför fungerade utmärkt men testar att föja MGs exempel
//

// De här raderna används senare för att koppla ihop med mongodb
//
// .connect('mongodb://host.docker.internal:27017/movies')
// .then (() => {
//   console.log('Mongodb connected and the server is up and running');
//   app.listen(80);
// })
// .catch((err) => console.error(err));
