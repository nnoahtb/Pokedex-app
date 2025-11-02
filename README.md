# Pokédex App

A simple application where you can search for Pokémon by name or ID.

## How to run

### 1. Clone the repository
```bash
git clone https://github.com/nnoahtb/pokedex-app.git
```

### 2. Run the backend
```bash
cd backend
dotnet run
```

Backend will start on `http://localhost:5071`.

### 3. Run the frontend
```bash
cd frontend
npm install
npm run dev
```

Frontend will start on `http://localhost:5173`.

> Note: The frontend is preconfigured to use a proxy in `vite.config.ts`, so all requests to `/api/...` are automatically forwarded to the backend running on `http://localhost:5071`.

## API Endpoint

```http
GET /api/pokemon/{idOrName}
```
