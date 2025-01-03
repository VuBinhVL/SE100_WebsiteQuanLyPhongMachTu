import React, { useEffect, useState } from "react";
import ExamCard from "../../../../components/Customer/Service/ExamCard"; // Component bạn đã tạo
import "./MedicalExamList.css";
import Anh from "../../../../assets/images/clinic1.png";
import SearchIcon from "../../../../assets/icons/icon-search.png";
import { fetchGet, fetchPost } from "../../../../lib/httpHandler";
import { showErrorMessageBox } from "../../../../components/MessageBox/ErrorMessageBox/showErrorMessageBox";
import { showSuccessMessageBox } from "../../../../components/MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showYesNoMessageBox } from "../../../../components/MessageBox/YesNoMessageBox/showYesNoMessgeBox";
export default function MedicalExamList() {
  const [currentPage, setCurrentPage] = useState(1);
  const examsPerPage = 8; // Số lượng ca khám trên mỗi trang
  const [examList, setExamList] = useState([]); // Danh sách ca khám
  const [selectedDay, setSelectedDay] = useState("Tất cả");
  const [searchQuery, setSearchQuery] = useState("");

  //Gọi API lấy danh sách ca khám
  useEffect(() => {
    const uri = "/api/quan-li-ca-kham"; // Đường dẫn API
    fetchGet(
      uri,
      (data) => {
        console.log(data);
        // Lọc các ca khám có BacSiID khác null
        const filteredData = data.filter((exam) => exam.bacSiId !== null);
        console.log(filteredData);
        setExamList(filteredData); // Cập nhật danh sách ca khám
      },
      (error) => {
        showErrorMessageBox(error);
      },
      () => {
        showErrorMessageBox("Không thể kết nối đến server");
      }
    );
  }, []);

  //Định dạng ngày khám
  function formatDate(dateString) {
    const daysOfWeek = [
      "Chủ nhật",
      "Thứ 2",
      "Thứ 3",
      "Thứ 4",
      "Thứ 5",
      "Thứ 6",
      "Thứ 7",
    ];

    const date = new Date(dateString);
    const dayOfWeek = daysOfWeek[date.getDay()]; // Lấy thứ trong tuần
    const day = date.getDate().toString().padStart(2, "0"); // Lấy ngày
    const month = (date.getMonth() + 1).toString().padStart(2, "0"); // Lấy tháng
    const year = date.getFullYear(); // Lấy năm

    return `${dayOfWeek} ngày ${day}/${month}/${year}`;
  }

  // Hàm xử lý khi tìm kiếm
  const handleSearchChange = (e) => {
    setSearchQuery(e.target.value);
  };

  // Hàm xử lý khi thay đổi ngày
  const handleDayChange = (e) => {
    setSelectedDay(e.target.value);
  };
  // Lọc danh sách ca khám
  const filteredExams = examList.filter((exam) => {
    const query = searchQuery.toLowerCase();
    const doctorMatch = exam.tenBacSi.toLowerCase().includes(query);
    const groupMatch = exam.tenChuyenMon.toLowerCase().includes(query);

    const daysOfWeek = [
      "Chủ nhật",
      "Thứ 2",
      "Thứ 3",
      "Thứ 4",
      "Thứ 5",
      "Thứ 6",
      "Thứ 7",
    ];
    const examDate = new Date(exam.ngayKham); // Chuyển ngày khám thành Date object
    const examDay = daysOfWeek[examDate.getDay()]; // Lấy tên ngày từ ngày khám

    const dayMatch = selectedDay === "Tất cả" || examDay === selectedDay;

    return (doctorMatch || groupMatch) && dayMatch;
    return dayMatch;
  });

  //Đăng ký ca khám
  function registerExam(caKhamId) {
    const uri = "/api/quan-li-ca-kham/dang-ky"; // Đường dẫn API
    const body = { caKhamId }; // Payload phải chứa trường "caKhamId"

    fetchPost(
      uri,
      body,
      (response) => {
        // Xử lý khi đăng ký thành công
        showSuccessMessageBox("Đăng ký thành công ca khám!");
      },
      (error) => {
        // Xử lý khi có lỗi xảy ra
        showErrorMessageBox(error.message || "Đăng ký thất bại.");
      },
      () => {
        // Xử lý khi không thể kết nối đến server
        showErrorMessageBox("Không thể kết nối đến server.");
      }
    );
  }

  // Phân trang
  const indexOfLastExam = currentPage * examsPerPage;
  const indexOfFirstExam = indexOfLastExam - examsPerPage;
  const currentExams = filteredExams.slice(indexOfFirstExam, indexOfLastExam);

  const totalPages = Math.ceil(filteredExams.length / examsPerPage);

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
          Danh sách các ca khám đang mở trong tuần
        </h2>
        {/* Tìm kiếm và lọc */}
        <div className="filter-section">
          <div className="search-bar-container">
            <input
              type="text"
              className="search-bar"
              placeholder="Nhập tên bác sĩ, nhóm bệnh để tìm kiếm"
              value={searchQuery} // Liên kết state
              onChange={handleSearchChange} // Gọi hàm khi người dùng nhập
            />
            <img src={SearchIcon} alt="Tìm kiếm" className="search-icon" />
          </div>

          <select
            className="day-filter"
            value={selectedDay}
            onChange={handleDayChange}
          >
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
          {filteredExams.map((exam) => (
            <ExamCard
              key={exam.id}
              group={exam.tenChuyenMon} // Nhóm bệnh
              doctorName={exam.tenBacSi}
              starttime={exam.thoiGianBatDau.slice(0, 5)} // Cắt chỉ lấy HH:mm
              endtime={exam.thoiGianKetThuc.slice(0, 5)} // Cắt chỉ lấy HH:mm
              date={formatDate(exam.ngayKham)} // Định dạng ngày
              image={exam.image} // Ảnh bác sĩ
              onRegister={() => registerExam(exam.id)} // Gọi API với exam.id làm "caKhamId"
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
