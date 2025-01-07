import React from "react";
import "./Doctor.css";
import Anh from "../../../assets/images/clinic1.png";
import DoctorCard from "../../../components/Customer/Doctor/DoctorCard";
import "../../../styles/index.css";

// Dữ liệu mẫu cho các bác sĩ
const doctorsData = [
  {
    image:
      "https://vwu.vn/documents/20182/3458479/28_Feb_2022_115842_GMTbsi_thuhien.jpg/c04e15ea-fbe4-415f-bacc-4e5d4cc0204d",
    name: "Trần Thanh Trúc",
    details: [
      "+ Hơn 30 năm kinh nghiệm trong nghề.",
      "+ Chuyên gia điều trị bệnh tim mạch.",
      "+ Thành công với hơn 3000 ca bệnh.",
    ],
  },
  {
    image:
      "https://files.benhvien108.vn/ecm/source_files/2019/02/12/190212-2-090611-120219-57.jpg",
    name: "Nguyễn Minh Tâm",
    details: [
      "+ Hơn 20 năm kinh nghiệm trong nghề.",
      "+ Chuyên gia điều trị bệnh phổi.",
      "+ Thành công với hơn 1000 ca bệnh.",
    ],
  },
  {
    image:
      "https://images.hcmcpv.org.vn/res/news/2021/04/27-04-2021-nu-bac-si-tre-voi-niem-dam-me-nghien-cuu-khoa-hoc-3BF6CE8.jpg",
    name: "Lê Hoàng Yến",
    details: [
      "+ Hơn 5 năm kinh nghiệm trong nghề.",
      "+ Chuyên gia điều trị bệnh hô hấp.",
      "+ Thành công với hơn 8000 ca bệnh.",
    ],
  },
];

export default function Doctor() {
  return (
    <div className="doctor-page">
      {/* Phần giới thiệu đội ngũ bác sĩ */}
      <div className="doctor-section">
        <div className="doctor-content">
          <h1 className="doctor-title">Đội ngũ bác sĩ chuyên nghiệp</h1>
          <p className="doctor-description">
            Hãy cùng chúng tôi khám phá đội ngũ bác sĩ hàng đầu tại phòng khám
            nơi hội tụ những chuyên gia y tế với kinh nghiệm phong phú và tinh
            thần tận tâm. Chúng tôi luôn nỗ lực mang đến sự chăm sóc chu đáo và
            giải pháp y tế hiệu quả, nhằm đảm bảo sức khỏe và sự hài lòng tối đa
            cho mỗi bệnh nhân.
          </p>
        </div>
        <div className="doctor-image">
          <img className="image" src={Anh} alt="Hoạt họa" />
        </div>
      </div>

      {/* Phần danh sách bác sĩ */}
      <div className="doctor-highlight">
        <h2 className="highlight-title">BÁC SĨ NỔI BẬT</h2>
        <div className="doctor-grid">
          {doctorsData.map((doctor, index) => (
            <DoctorCard
              key={index}
              image={doctor.image}
              name={doctor.name}
              details={doctor.details}
            />
          ))}
        </div>
      </div>
    </div>
  );
}
