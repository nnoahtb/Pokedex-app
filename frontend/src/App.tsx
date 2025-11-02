import React, { useState } from "react";
import SearchForm from "./components/SearchForm";
import PokemonCard from "./components/PokemonCard";

interface Pokemon {
  id: number;
  name: string;
  image: string;
}

const App: React.FC = () => {
  const [pokemon, setPokemon] = useState<Pokemon | null>(null);
  const [error, setError] = useState<boolean>(false);

  const handleSearch = async (query: string) => {
    setError(false);
    setPokemon(null);

    try {
      const res = await fetch(`/api/pokemon/${encodeURIComponent(query)}`);
      if (res.status === 404) {
        setError(true);
        return;
      }
      if (!res.ok) {
        setError(true);
        return;
      }
      const data: Pokemon = await res.json();
      setPokemon(data);
    } catch (e) {
      console.error("Fetch error:", e);
      setError(true);
    }
  };

  return (
    <div className="container">
      <h1>Pokédex</h1>
      <PokemonCard pokemon={pokemon} error={error} />
      <SearchForm onSearch={handleSearch} />
    </div>
  );
};

export default App;
