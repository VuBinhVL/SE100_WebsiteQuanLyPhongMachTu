import React from "react";
import PropTypes from "prop-types";
import "./Textbox.css";

export default function Textbox({ icon, placeholder, type, value, onChange }) {
  return (
    <div className="input-with-icon">
      <div className="icon-container">
        <img src={icon} alt="icon" className="icon" />
      </div>
      <input
        type={type || "text"} // Loại input, mặc định là "text"
        className="input-field"
        placeholder={placeholder}
        value={value}
        onChange={onChange}
      />
    </div>
  );
}

// Xác định kiểu dữ liệu cho các props
Textbox.propTypes = {
  icon: PropTypes.string.isRequired, // URL hoặc đường dẫn ảnh icon
  placeholder: PropTypes.string, // Placeholder cho input
  type: PropTypes.string, // Loại input (text, email, password, v.v.)
  value: PropTypes.string, // Giá trị của input
  onChange: PropTypes.func, // Hàm xử lý khi nội dung input thay đổi
};

//  Đặt giá trị mặc định cho các props
// InputWithIcon.defaultProps = {
//   placeholder: "",
//   type: "text",
//   value: "",
//   onChange: () => {},
// };
