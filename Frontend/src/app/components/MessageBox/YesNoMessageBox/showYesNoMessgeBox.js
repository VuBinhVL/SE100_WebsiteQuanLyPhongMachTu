import React from "react";
import { createRoot } from "react-dom/client";
import YesNoMessageBox from "./YesNoMessageBox";

export function showYesNoMessageBox(title) {
  return new Promise((resolve) => {
    // Tạo một container DOM mới
    const container = document.createElement("div");
    document.body.appendChild(container);

    // Tạo root React
    const root = createRoot(container);

    // Hàm xử lý đóng và xóa MessageBox
    const handleClose = (isYes) => {
      root.unmount(); // Gỡ bỏ component
      document.body.removeChild(container); // Xóa container
      resolve(isYes); // Trả về kết quả YES hoặc NO
    };

    // Render YesNoMessageBox
    root.render(
      <YesNoMessageBox
        title={title}
        onYes={() => handleClose(true)} // YES trả về true
        onNo={() => handleClose(false)} // NO trả về false
      />
    );
  });
}
