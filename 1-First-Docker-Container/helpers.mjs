const connectToDatabase = () => {
  const promise = new Promise((resolve, reject) => {
    setTimeout(() => {
      resolve();
    }, 1000);
  });

  return promise;
};

export default connectToDatabase;
