import React from "react";
import { createRoot } from "react-dom/client";
import SuccessMessageBox from "./SuccessMessageBox";

export function showSuccessMessageBox(title) {
  // Tạo một container DOM mới
  const container = document.createElement("div");
  document.body.appendChild(container);

  // Tạo root React
  const root = createRoot(container);

  // Hàm đóng và dọn dẹp
  const handleClose = () => {
    root.unmount(); // Gỡ bỏ component khỏi DOM
    document.body.removeChild(container); // Xóa container
  };

  // Render SuccessMessageBox
  root.render(<SuccessMessageBox title={title} onClose={handleClose} />);
}
