import React from "react";
import { Link } from "react-router-dom";
import "./Forbidden.css";

export default function Forbidden() {
  return (
    <div className="forbidden">
      <h1>403</h1>
      <p>Sorry, you do not have permission to access this page.</p>
      <Link to="/">Go to Home</Link>
    </div>
  );
}