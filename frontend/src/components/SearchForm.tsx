import React, { useState } from "react";

interface Props {
  onSearch: (query: string) => void;
}

export default function SearchForm({ onSearch }: Props) {
  const [input, setInput] = useState("");

  const submit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!input) return;
    onSearch(input.trim());
  };

  return (
    <form onSubmit={submit} className="search-form">
      <input
        value={input}
        onChange={(e) => setInput(e.target.value)}
        placeholder="Enter name or ID"
        className="search-input"
        aria-label="Search for Pokemon"
      />
      <button type="submit" className="search-button">
        Search
      </button>
    </form>
  );
}
