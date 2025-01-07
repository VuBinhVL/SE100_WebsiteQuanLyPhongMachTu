export const formatDate = (dateString) => {
  // Chuyển đổi chuỗi ngày từ "YYYY-MM-DDTHH:mm:ss" thành "YYYY-MM-DD"
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = ("0" + (date.getMonth() + 1)).slice(-2); // Thêm 1 vì tháng bắt đầu từ 0
  const day = ("0" + date.getDate()).slice(-2); // Đảm bảo ngày luôn có 2 chữ số
  return `${year}-${month}-${day}`; // Định dạng theo kiểu "YYYY-MM-DD"
};
