import React from "react";
import "./AccountTextBox.css";

export default function AccountTextBox({
  label = "Label",
  type = "text",
  value,
  onChange,
  disabled = false, // Mặc định không bị vô hiệu hóa
}) {
  return (
    <div className="account-textbox-input-field">
      <label className="input-label">{label}</label>
      <input
        className="input-box"
        type={type}
        value={value}
        onChange={onChange}
        disabled={disabled} // Điều khiển trạng thái của input
        placeholder=""
      />
    </div>
  );
}
