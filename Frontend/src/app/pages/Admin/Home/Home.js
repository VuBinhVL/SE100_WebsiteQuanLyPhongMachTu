import React from "react";
import { sCount } from "./homeStore";
import "../../Admin/Home/home.css"

export default function Home() {
  const count = sCount.use();

  const handleClick = () => {
    sCount.set((n) => (n.value += 1));
  };

  return (
    <div className="xx">
      <h1>Home {count}</h1>
      <button onClick={handleClick}>Up</button>
    </div>
  );
}
