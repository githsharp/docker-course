import { useEffect, useState } from 'react';
import './colors.css';
import './utilities.css';
import './site.css';
import './navbar.css';
import Navbar from './components/Navbar';

function App() {
  const [movies, setMovies] = useState([]);

  useEffect(() => {
    loadMovies();
  }, []);

  const loadMovies = () => {
    fetch('http://localhost/api/movies')
      .then((response) => response.json())
      .then((result) => {
        console.log(result.data);
        setMovies(result.data);
      });
  };

  return (
    <>
      <Navbar />
      <main>
        <section className='container'>
          <h2>Populära filmer</h2>
          <div className='grid'>
            {movies.map((movie) => (
              <div className='card' key={movie.id}>
                <img src={`https://image.tmdb.org/t/p/w500${movie.poster_path}`} alt='Movie Titel' />
                <div className='card-body'>
                  <h5 className='card-title'>{movie.title}</h5>
                  <p className='card-text'>
                    <small className='text-muted'>Premiär datum: {movie.release_date}</small>
                  </p>
                </div>
              </div>
            ))}
          </div>
        </section>
      </main>
    </>
  );
}

export default App;
