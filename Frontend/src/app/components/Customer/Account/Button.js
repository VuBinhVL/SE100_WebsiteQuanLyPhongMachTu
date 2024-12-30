import React from "react";
import "./Button.css";

export default function Button({
  label = "Xác nhận",
  color = "#348f6c",
  onClick,
}) {
  return (
    <button
      className="account-button"
      style={{ backgroundColor: color }}
      onClick={onClick}
    >
      {label}
    </button>
  );
}
