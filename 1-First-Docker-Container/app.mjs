import express from 'express';
import connectToDatabase from './helpers.mjs';

const app = express();

app.get('/', (req, res) => {
  res.status(200).json({ message: 'It is working' });
});

await connectToDatabase();
app.listen(3000, () => console.log('Server is up and running'));
