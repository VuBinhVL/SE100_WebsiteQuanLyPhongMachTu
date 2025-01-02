import React from "react";
import Button from "../../../components/Customer/HomePage/Button";
import Card from "../../../components/Customer/HomePage/Card";
import DiseaseCard from "../../../components/Customer/HomePage/DiseaseCard";
import StaffCard from "../../../components/Customer/HomePage/StaffCard";
import Anh from "../../../assets/images/clinic1.png";
import Meeting from "../../../assets/images/meeting.png";
import Urgent from "../../../assets/images/urgent.png";
import Serive_24_7 from "../../../assets/images/24_7.png";
import Clinic from "../../../assets/images/clinic2.png";
import Clinic2 from "../../../assets/images/clinic3.png";
import TieuHoa from "../../../assets/images/tieuhoa.png";
import TimMach from "../../../assets/images/timmach.png";
import HoHap from "../../../assets/images/hohap.png";
import DaLieu from "../../../assets/images/dalieu.png";
import "./CustomerHome.css";
import "../../../styles/index.css";

// Phần danh sách các bệnh điều trị
const diseaseCardsData = [
  {
    img: TimMach,
    title: "Tim mạch",
    description:
      "Chúng tôi cung cấp dịch vụ chăm sóc sức khỏe tim mạch chuyên sâu với đội ngũ bác sĩ giàu kinh nghiệm.",
  },
  {
    img: HoHap,
    title: "Hô hấp",
    description:
      "Chăm sóc các bệnh về đường hô hấp với các giải pháp hiện đại và hiệu quả nhất.",
  },
  {
    img: TieuHoa,
    title: "Tiêu hóa",
    description:
      "Đội ngũ chuyên gia sẵn sàng hỗ trợ bạn trong việc chăm sóc sức khỏe tiêu hóa.",
  },
  {
    img: DaLieu,
    title: "Da liễu",
    description:
      "Chuyên gia da liễu sẽ giúp bạn giải quyết mọi vấn đề liên quan đến sức khỏe làn da.",
  },
];

// Danh sách bác sĩ
const staffCardsData = [
  {
    avatar: Clinic,
    name: "Tiến Đạt",
    role: "Nhân viên",
    specialty: "Tim mạch",
  },
  {
    avatar: Clinic,
    name: "Ngọc Lan",
    role: "Bác sĩ",
    specialty: "Da liễu",
  },
  {
    avatar: Clinic,
    name: "Hoàng Nam",
    role: "Chuyên gia",
    specialty: "Tiêu hóa",
  },
  {
    avatar: Clinic,
    name: "Hoàng Nam",
    role: "Chuyên gia",
    specialty: "Tiêu hóa",
  },
  {
    avatar: Clinic,
    name: "Hoàng Nam",
    role: "Chuyên gia",
    specialty: "Tiêu hóa",
  },
  {
    avatar: Clinic,
    name: "Hoàng Nam",
    role: "Chuyên gia",
    specialty: "Tiêu hóa",
  },
  {
    avatar: Clinic,
    name: "Hoàng Nam",
    role: "Chuyên gia",
    specialty: "Tiêu hóa",
  },
];

export default function CustomerHome() {
  return (
    <div className="homepage">
      {/* Phần Hero */}
      <div className="homepage-container">
        {/* Phần bên trái: Nội dung */}
        <div className="homepage-content">
          <h2 className="homepage-subtitle">Cuộc sống tốt đẹp hơn</h2>
          <h1 className="homepage-title">Chăm sóc sức khỏe tốt hơn</h1>
          <p className="homepage-description">
            Hãy tham gia cùng chúng tôi tại Private Clinic trong một không gian
            chăm sóc sức khỏe vui vẻ và thân thiện. Đội ngũ chuyên gia của chúng
            tôi luôn nỗ lực hết mình để mang lại sự hài lòng cho bạn. Chúng tôi
            cam kết tận tâm với sứ mệnh chăm sóc sức khỏe của bạn!
          </p>
          <div className="homepage-buttons">
            <Button
              text="Đăng kí khám"
              variant="primary"
              style={{
                backgroundColor: "#FF8C5E",
                color: "#ffffff",
                border: "1px solid #ffffff",
                fontWeight: 500,
              }}
              onClick={() => console.log("Đăng kí khám")}
            />
            <Button
              text="Tìm hiểu thêm"
              variant="outline-primary"
              style={{
                backgroundColor: "transparent",
                color: "#ffffff",
                border: "1px solid #ffffff",
                fontWeight: 500,
              }}
              onClick={() => console.log("Tìm hiểu thêm")}
            />
          </div>
        </div>

        {/* Phần bên phải: Hình ảnh */}
        <div className="homepage-image">
          <img className="image" src={Anh} alt="Hoạt họa phòng khám" />
        </div>
      </div>
      {/* Phần card */}
      <div className="homepage-services">
        <Card
          img={Meeting}
          title="Hẹn gặp dễ dàng"
          description="Đặt lịch hẹn với bác sĩ chỉ trong vài bước đơn giản. Chúng tôi cam kết mang lại sự thuận tiện và tiết kiệm thời gian cho bạn."
        />
        <Card
          img={Urgent}
          title="Dịch vụ khẩn cấp"
          description="Hỗ trợ chăm sóc sức khỏe khẩn cấp 24/7, luôn sẵn sàng giúp đỡ trong những tình huống cần thiết nhất."
        />
        <Card
          img={Serive_24_7}
          title="Dịch vụ 24/7"
          description="Chăm sóc sức khỏe toàn diện mọi lúc, mọi nơi. Đội ngũ bác sĩ và nhân viên của chúng tôi luôn sẵn sàng phục vụ bạn bất kể thời gian."
        />
      </div>
      {/* Phần giới thiệu */}
      <div className="homepage-introduction">
        <div className="introduction-image">
          <img src={Clinic} alt="Phòng khám chuyên nghiệp" />
        </div>
        <div className="introduction-content">
          <h2>Chào mừng đến với Private Clinic</h2>
          <p>
            Chào mừng bạn đến với phòng khám tư Private Clinic, nơi mang đến
            dịch vụ chăm sóc sức khỏe tận tình và chuyên nghiệp. Chúng tôi cam
            kết biến mỗi cuộc hẹn của bạn trở nên dễ chịu và thoải mái. Đội ngũ
            bác sĩ của chúng tôi luôn sẵn sàng lắng nghe và đáp ứng nhu cầu của
            bạn với sự chu đáo nhất!
          </p>
        </div>
      </div>
      {/* Phần điều trị */}
      <div className="homepage-treatment">
        <h2 className="treatment-title">Điều trị</h2>
        <div className="treatment-grid">
          {diseaseCardsData.map((card, index) => (
            <DiseaseCard
              key={index}
              img={card.img}
              title={card.title}
              description={card.description}
            />
          ))}
        </div>
      </div>
      {/* Phần Bác sĩ */}
      <div className="homepage-staff">
        <h2 className="staff-title">Bác sĩ của chúng tôi</h2>
        <div className="staff-row">
          {staffCardsData.map((staff, index) => (
            <StaffCard
              key={index}
              avatar={staff.avatar}
              name={staff.name}
              role={staff.role}
              specialty={staff.specialty}
            />
          ))}
        </div>
      </div>

      {/* Phần đặt lịch hẹn */}
      <div className="homepage-booking">
        <div className="booking-content">
          <h2>Đặt lịch hẹn ngay hôm nay để chăm sóc sức khỏe tốt nhất!</h2>
          <Button
            text="Đăng kí khám"
            variant="primary"
            style={{
              backgroundColor: "#FF8C5E",
              color: "#ffffff",
              border: "1px solid #ffffff",
              fontWeight: 500,
            }}
            onClick={() => console.log("Đăng kí khám")}
          />
        </div>
        <div className="booking-image">
          <img src={Clinic2} alt="Đặt lịch hẹn" />
        </div>
      </div>
    </div>
  );
}
