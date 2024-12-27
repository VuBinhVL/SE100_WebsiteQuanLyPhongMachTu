import React from "react";
import PropTypes from "prop-types";

export default function Button({
  text,
  onClick,
  type = "button",
  variant = "primary",
  style = {},
}) {
  return (
    <button
      className={`btn btn-${variant}`}
      type={type}
      onClick={onClick}
      style={style}
    >
      {text}
    </button>
  );
}

// Định nghĩa kiểu dữ liệu cho props
Button.propTypes = {
  text: PropTypes.string.isRequired, // Nội dung của nút
  onClick: PropTypes.func, // Hàm được gọi khi nút được nhấn
  type: PropTypes.oneOf(["button", "submit", "reset"]), // Loại của nút
  variant: PropTypes.oneOf(["primary", "secondary", "danger", "success"]), // Kiểu nút Bootstrap
};

// Đặt giá trị mặc định cho props
Button.defaultProps = {
  onClick: () => {}, // Hàm mặc định nếu không có onClick
  type: "button", // Mặc định là nút thường
  variant: "primary", // Kiểu mặc định là primary
};
