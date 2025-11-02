import React from "react";

interface Pokemon {
  id: number;
  name: string;
  image: string;
}

interface Props {
  pokemon: Pokemon | null;
  error: boolean;
}

const PokemonCard: React.FC<Props> = ({ pokemon, error }) => {
  if (error) {
   return <div className="pokemon-error">Pokémon not found</div>;
  }

  if (!pokemon) return null;

    return (
    <div className="pokemon-card">
      <h2>
        {pokemon.name} #{pokemon.id}
      </h2>
      {pokemon.image ? (
        <img src={pokemon.image} alt={pokemon.name} />
      ) : (
        <div>No image available</div>
      )}
    </div>
  );
};

export default PokemonCard;
