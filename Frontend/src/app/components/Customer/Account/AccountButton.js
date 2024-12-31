import React from "react";
import "./AccountButton.css";

export default function AccountButton({
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
