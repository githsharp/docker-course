# Multi-Container applikation

**Instruktioner för att köra applikationen**

- Hämta en api-key ifrån themoviedb.org och ange den på alla ställen där anrop sker till themoviedb.org. Ni hittar detta i koden där det står &lt;_you api-key_&gt;
- I ett terminal fönster skapar ni ett nytt Docker nätverk
  `docker network create movieflix-net`
- Efter det så kör följande kommando `docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=< strong password>' -p 1433:1433 --network movieflix-net --name movieflix-server -d mcr.microsoft.com/azure-sql-edge`
- Skapa en image för movieflix api `docker build -t movieflix-api .`
  **OBSERVERA ATT NO MÅSTE STÅ I KATALOGEN FÖR MOVIEFLIX-API!\***
- Skapa en container för REST API'et
  `docker run -d --rm --name movieflix-backend --network movieflix-net -p 5001:5001 movieflix-api`
- Skapa en image för frontend applikationen
  `docker build -t movieflix-app .`
- Skapa en container för frontend applikationen
  `docker run -d --rm --name movieflix-frontend --network movieflix-net -p 5011:5011 movieflix-app`

Öppna upp en webbläsare och gå till adressen http://localhost:5011/home och ni bör se en sida med de 20 första filmerna som är hämtade ifrån themoviedb.org.
