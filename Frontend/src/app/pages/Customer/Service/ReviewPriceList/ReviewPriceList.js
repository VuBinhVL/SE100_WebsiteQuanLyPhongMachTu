import React from "react";
import Anh from "../../../../assets/images/clinic1.png";
import "./ReviewPriceList.css";

export default function ReiviewPriceList() {
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
              {/* Nhóm bệnh: Tim mạch */}
              <tr>
                <td rowSpan="5">Tim mạch</td>
                <td>Bệnh mạch vành</td>
                <td>120.000 VND</td>
              </tr>
              <tr>
                <td>Cao huyết áp</td>
                <td>90.000 VND</td>
              </tr>
              <tr>
                <td>Suy tim</td>
                <td>100.000 VND</td>
              </tr>
              <tr>
                <td>Bệnh van tim</td>
                <td>225.000 VND</td>
              </tr>
              <tr>
                <td>Hẹp động mạch chủ</td>
                <td>125.000 VND</td>
              </tr>

              {/* Nhóm bệnh: Hô hấp */}
              <tr>
                <td rowSpan="3">Hô hấp</td>
                <td>Hen suyễn</td>
                <td>100.000 VND</td>
              </tr>
              <tr>
                <td>Phổi tắc nghẽn mãn tính</td>
                <td>125.000 VND</td>
              </tr>
              <tr>
                <td>Lao phổi</td>
                <td>900.000 VND</td>
              </tr>

              {/* Nhóm bệnh: Tiêu hóa */}
              <tr>
                <td rowSpan="4">Tiêu hóa</td>
                <td>Viêm loét dạ dày</td>
                <td>400.000 VND</td>
              </tr>
              <tr>
                <td>Trào ngược dạ dày</td>
                <td>50.000 VND</td>
              </tr>
              <tr>
                <td>Hội chứng ruột kích thích</td>
                <td>75.000 VND</td>
              </tr>
              <tr>
                <td>Sỏi mật</td>
                <td>699.999 VND</td>
              </tr>
            </tbody>
          </table>
          <p className="price-note">
            *** Lưu ý: Giá tham khảo đã bao gồm chi phí khám và các xét nghiệm
            liên quan, nhưng không bao gồm chi phí thuốc. Trong quá trình khám,
            có thể sẽ phát sinh thêm chi phí tùy thuộc vào mức độ của bệnh.
          </p>
        </div>
      </div>
    </div>
  );
}
