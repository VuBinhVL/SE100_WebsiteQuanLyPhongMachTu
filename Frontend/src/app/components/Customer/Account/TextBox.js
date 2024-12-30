import React from "react";
import "./TextBox.css";

export default function Textbox({
  label = "Label",
  type = "text",
  value,
  onChange,
}) {
  return (
    <div className="input-field">
      <label className="input-label">{label}</label>
      <input
        className="input-box"
        type={type}
        value={value}
        onChange={onChange}
        placeholder=""
      />
    </div>
  );
}
