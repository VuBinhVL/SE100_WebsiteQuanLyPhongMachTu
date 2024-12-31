import React from "react";
import { createRoot } from "react-dom/client";
import DeleteMessageBox from "./DeleteMessageBox";

export function showDeleteMessageBox(title, onConfirm) {
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

  // Render ErrorMessageBox
  root.render(<DeleteMessageBox title={title} onClose={handleClose} onConfirm={() => {
    onConfirm(); // Gọi callback xác nhận
    handleClose(); // Đóng MessageBox
  }} />);
}
