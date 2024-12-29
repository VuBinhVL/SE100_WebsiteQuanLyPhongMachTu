import React from "react";
import ReactDOM from "react-dom";
import SuccessMessageBox from "./SuccessMessageBox";

export function showSuccessMessageBox(title) {
  // Tạo một container DOM mới
  const container = document.createElement("div");
  document.body.appendChild(container);

  // Hàm đóng và dọn dẹp
  const handleClose = () => {
    ReactDOM.unmountComponentAtNode(container); // Gỡ component khỏi DOM
    document.body.removeChild(container); // Xóa container
  };

  // Render SuccessMessageBox
  ReactDOM.render(
    <SuccessMessageBox title={title} onClose={handleClose} />,
    container
  );
}
