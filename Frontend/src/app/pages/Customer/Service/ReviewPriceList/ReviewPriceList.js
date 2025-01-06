import React, { useEffect, useState } from "react";
import Anh from "../../../../assets/images/clinic1.png";
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";
import { fetchGet } from "../../../../lib/httpHandler";
import DiseaseInfoPopup from "../DiseaseInfoPopup/DiseaseInfoPopup";
import "./ReviewPriceList.css";

export default function ReiviewPriceList() {
  const [table, setTable] = useState([]); // Danh sách bảng giá dịch vụ
  const [selectedDisease, setSelectedDisease] = useState(null); // Bệnh lý được chọn
  const [isPopupOpen, setIsPopupOpen] = useState(false); // Trạng thái hiển thị popup
  //Gọi API lấy bảng giá dịch vụ
  useEffect(() => {
    const uri = "/api/quan-li-benh-ly/bang-gia-benh-ly";
    fetchGet(
      uri,
      (data) => {
        console.log(data);
        // Nhóm dữ liệu theo "tenNhomBenh"
        const groupedData = data.reduce((acc, curr) => {
          const groupName = curr.tenNhomBenh;
          if (!acc[groupName]) {
            acc[groupName] = [];
          }
          acc[groupName].push(curr);
          return acc;
        }, {});
        setTable(groupedData); // Cập nhật danh sách bảng giá dịch vụ
      },
      (error) => {
        showErrorMessageBox(error);
      },
      () => {
        showErrorMessageBox("Không thể kết nối đến server");
      }
    );
  }, []);

  // Xử lý khi double click vào một bệnh lý
  const handleRowDoubleClick = (disease) => {
    setSelectedDisease(disease.id); // Lưu thông tin bệnh lý
    setIsPopupOpen(true); // Mở popup
  };

  // Đóng popup
  const handleClosePopup = () => {
    setIsPopupOpen(false);
  };

  return (
    <div className="price-page">
      {/* Phần giới thiệu đội bảng giá dịch vụ */}
      <div className="price-section">
        <div className="price-content">
          <h1 className="price-title">Dịch vụ Y Tế Chất Lượng Cao</h1>
          <p className="price-description">
            Tại đây, chúng tôi cung cấp các dịch vụ khám bệnh đa dạng và chuyên
            nghiệp với mức giá tham khảo rõ ràng, minh bạch. Đội ngũ y bác sĩ
            giàu kinh nghiệm cùng trang thiết bị hiện đại cam kết mang đến sự
            hài lòng và an tâm cho bạn và gia đình trong việc chăm sóc sức khỏe.
          </p>
        </div>
        <div className="price-image">
          <img className="image" src={Anh} alt="Hoạt họa" />
        </div>
      </div>

      {/* Phần bảng giá dịch vụ */}
      <div className="price-highlight">
        <h2 className="price-highlight-title">
          Bảng giá, dịch vụ khám mới nhất
        </h2>
        <div className="price-grid">
          <table className="price-table">
            <thead>
              <tr>
                <th>Nhóm bệnh</th>
                <th>Bệnh lý</th>
                <th>Giá tham khảo</th>
              </tr>
            </thead>
            <tbody>
              {Object.keys(table).map((groupName, groupIndex) => (
                <>
                  {table[groupName].map((disease, diseaseIndex) => (
                    <tr
                      key={diseaseIndex}
                      onDoubleClick={() => handleRowDoubleClick(disease)} // Gắn sự kiện double click
                      style={{ cursor: "pointer" }}
                    >
                      {diseaseIndex === 0 && (
                        <td rowSpan={table[groupName].length}>{groupName}</td>
                      )}
                      <td>{disease.tenBenhLy}</td>
                      <td>{disease.giaThamKhao.toLocaleString()} VND</td>
                    </tr>
                  ))}
                </>
              ))}
            </tbody>
          </table>
          <p className="price-note">
            *** Lưu ý: Giá tham khảo đã bao gồm chi phí khám và các xét nghiệm
            liên quan, nhưng không bao gồm chi phí thuốc. Trong quá trình khám,
            có thể sẽ phát sinh thêm chi phí tùy thuộc vào mức độ của bệnh.
          </p>
        </div>
      </div>
      {/* Hiển thị popup nếu mở */}
      {isPopupOpen && (
        <DiseaseInfoPopup
          diseaseId={selectedDisease} // Chỉ truyền id
          onClose={handleClosePopup}
        />
      )}
    </div>
  );
}
