import React, { useState } from "react";
import ExamCard from "../../../../components/Customer/Service/ExamCard"; // Component bạn đã tạo
import "./MedicalExamList.css";
import Anh from "../../../../assets/images/clinic1.png";
import SearchIcon from "../../../../assets/icons/icon-search.png";

export default function MedicalExamList() {
  const [currentPage, setCurrentPage] = useState(1);
  const examsPerPage = 8; // Số lượng ca khám trên mỗi trang

  // Dữ liệu giả lập danh sách ca khám
  const examList = Array.from({ length: 18 }, (_, index) => ({
    id: index + 1,
    doctorName: "TS.BS Nguyễn Văn A",
    group: "Tim mạch",
    time: "14:30-16:30",
    date: "Thứ 2 ngày 09/12/2024",
    image:
      "https://img.freepik.com/premium-vector/doctor-icon-avatar-white_136162-58.jpg",
  }));

  const indexOfLastExam = currentPage * examsPerPage;
  const indexOfFirstExam = indexOfLastExam - examsPerPage;
  const currentExams = examList.slice(indexOfFirstExam, indexOfLastExam);

  const totalPages = Math.ceil(examList.length / examsPerPage);

  const handlePageChange = (pageNumber) => {
    setCurrentPage(pageNumber);
  };

  return (
    <div className="medical-exam-list-page">
      {/* Phần giới thiệu đăng ký ca khám */}
      <div className="medical-exam-list-section">
        <div className="medical-exam-list-content">
          <h1 className="medical-exam-list-title">Đăng Ký Khám Nhanh Chóng</h1>
          <p className="medical-exam-list-description">
            Tại đây, chúng tôi cung cấp hệ thống đăng ký khám tiện lợi và nhanh
            chóng, giúp bạn dễ dàng chọn lựa dịch vụ y tế phù hợp. Đội ngũ y bác
            sĩ giàu kinh nghiệm và trang thiết bị hiện đại cam kết mang lại chất
            lượng dịch vụ tốt nhất, đảm bảo sức khỏe và sự hài lòng cho bạn và
            gia đình.
          </p>
        </div>
        <div className="medical-exam-list-image">
          <img className="image" src={Anh} alt="Hoạt họa" />
        </div>
      </div>

      {/* Phần danh    */}
      <div className="medical-exam-list-highlight">
        <h2 className="medical-exam-list-highlight-title">
          Danh sách các ca khám đang mở
        </h2>
        {/* Tìm kiếm và lọc */}
        <div className="filter-section">
          <div className="search-bar-container">
            <input
              type="text"
              className="search-bar"
              placeholder="Nhập tên bác sĩ, nhóm bệnh để tìm kiếm"
            />
            <img src={SearchIcon} alt="Tìm kiếm" className="search-icon" />
          </div>

          <select className="day-filter">
            <option value="Tất cả">Tất cả</option>
            <option value="Thứ 2">Thứ 2</option>
            <option value="Thứ 3">Thứ 3</option>
            <option value="Thứ 4">Thứ 4</option>
            <option value="Thứ 5">Thứ 5</option>
            <option value="Thứ 6">Thứ 6</option>
            <option value="Thứ 7">Thứ 7</option>
            <option value="Chủ nhật">Chủ nhật</option>
          </select>
        </div>

        {/* Danh sách các ca khám */}
        <div className="medical-exam-list-grid">
          {currentExams.map((exam) => (
            <ExamCard
              key={exam.id}
              group={exam.group}
              doctorName={exam.doctorName}
              time={exam.time}
              date={exam.date}
              image={exam.image}
              onRegister={() => alert(`Đăng ký ca khám ${exam.id}`)}
            />
          ))}
        </div>
      </div>
      {/* Phân trang */}
      <div className="pagination">
        {[...Array(totalPages)].map((_, index) => (
          <button
            key={index + 1}
            className={`pagination-button ${
              currentPage === index + 1 ? "active" : ""
            }`}
            onClick={() => handlePageChange(index + 1)}
          >
            {index + 1}
          </button>
        ))}
      </div>
    </div>
  );
}
