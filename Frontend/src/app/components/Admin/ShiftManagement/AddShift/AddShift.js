import React, { useEffect } from "react";
import { IoMdAddCircleOutline } from "react-icons/io";
import { IoIosArrowDown } from "react-icons/io";
import { useState } from "react"
import "./AddShift.css"
import { fetchGet, fetchPost } from "../../../../lib/httpHandler";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox"
export default function AddShift(props) {
    const { listStaff, setListStaff } = props
    const [specialization, setSpecialization] = useState([]);
    // const [dataFilter, setDataFilter] = useState("DEFAULT");
    const [dataForm, setDataForm] = useState({})
    //gọi api lấy chuyên môn (nhóm bệnh)
    useEffect(() => {
        const uri = "/api/admin/quan-li-nhom-benh";
        fetchGet(
            uri,
            (sus) => {
                setSpecialization(sus);
            },
            (fail) => {
                // alert(fail.message);
                showErrorMessageBox(fail.message)
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }, []);
    const handleChange = (e) => {
        const value = e.target.value;
        const name = e.target.name;
        setDataForm({
            ...dataForm, [name]: value
        })
    }
    // hàm lấy lại listStaff

    // Hàm lấy danh sách nhân viên
    const fetchStaffList = () => {
        const uri = "/api/admin/quan-li-nhan-vien";
        fetchGet(
            uri,
            (data) => {
                setListStaff(data); // Cập nhật danh sách nhân viên
            },
            (fail) => {
                showErrorMessageBox(fail.message)
            },
            () => {
                alert("An error occurred while fetching staff list.");
            }
        );
    };
    const handleSubmit = (event) => {
        event.preventDefault();
        const { hoTen, gioiTinh, ngaySinh, chuyenMonId, soDienThoai, email, diaChi, image } = dataForm;

        // Validate dữ liệu trước khi gửi
        if (!hoTen || !gioiTinh || !ngaySinh || !chuyenMonId || !soDienThoai || !email || !diaChi || !image) {
            // alert("Please fill in all the required fields!");
            showErrorMessageBox("Please fill in all the required fields!")
            return;
        }

        if (!email.endsWith("@gmail.com")) {
            // alert("Email must end with @gmail.com");
            showErrorMessageBox("Email must end with @gmail.com")
            return;
        }
        // Kiểm tra số điện thoại
        if (!/^\d+$/.test(soDienThoai)) {
            // alert("Phone number must be a number");
            showErrorMessageBox("Phone number must be a number")
            return;
        }

        // Kiểm tra độ dài số điện thoại
        if (soDienThoai.length < 10 || soDienThoai.length > 11) {
            // alert("Phone number must be between 10 and 11 digits");
            showErrorMessageBox("Phone number must be between 10 and 11 digits")
            return;
        }
        // Kiểm tra ngày sinh
        const ngaySinhDate = new Date(ngaySinh);
        const currentDate = new Date();
        const minDate = new Date('1950-01-01');

        if (ngaySinhDate >= currentDate) {
            // alert("Date of birth must be before today");
            showErrorMessageBox("Date of birth must be before today")
            return;
        }

        if (ngaySinhDate < minDate) {
            // alert("Date of birth must be after 1950");
            showErrorMessageBox("Date of birth must be after 1950")
            return;
        }
        // Kiểm tra tuổi
        const age = Math.floor((currentDate - ngaySinhDate) / (1000 * 60 * 60 * 24 * 365.25));
        if (age < 18) {
            // alert("You must be at least 18 years old");
            showErrorMessageBox("You must be at least 18 years old")
            return;
        }
        // Kiểm tra tính duy nhất của số điện thoại và email
        for (const element of listStaff) {
            if (element.soDienThoai === soDienThoai) {
                // alert("Phone number already exists");
                showErrorMessageBox("Phone number already exists")
                return;
            }
            if (element.email === email) {
                // alert("Email already exists");
                showErrorMessageBox("Email already exists")
                return;
            }
        }

        addStaff();
    };

    const getIdSpecialization = () => {
        const { chuyenMonId } = dataForm;
        const specializationItem = specialization.find(element => element.tenNhomBenh === chuyenMonId);
        return specializationItem ? specializationItem.id : null; // Trả về null nếu không tìm thấy
    };
    // hàm clear data được fill trong modal
    const handleClearData = () => {
        // Clear data
        setDataForm({});
        const inputs = document.querySelectorAll('#staticBackdrop input, #staticBackdrop select');
        inputs.forEach(input => {
            input.value = '';
        });
        const selects = document.querySelectorAll('#staticBackdrop select');
        selects.forEach(select => {
            select.value = 'DEFAULT';
        });
    };
    const addStaff = async () => {
        const uri = "/api/admin/quan-li-nhan-vien/add"
        const idOfSpecialization = getIdSpecialization();
        if (!idOfSpecialization) {
            // alert("Specialization not found!");
            showErrorMessageBox("Specialization not found!")
            return;
        }
        const newDataFormClient = { ...dataForm }; //data này chứa tên chuyên môn
        const newDataFormServer = { ...dataForm, chuyenMonId: idOfSpecialization }; //data này chứa id để đẩy lên server
        newDataFormServer.image = "a.png";
        newDataFormClient.image = "a.png";
        //Lấy tên chuyên môn
        const tenChuyenMon = newDataFormClient.chuyenMonId;

        // Chuyển đổi key từ `chuyenMonId` sang `tenChuyenMon` để phù hợp với `listStaff`
        const transformedData = {
            ...newDataFormClient,
            tenChuyenMon: tenChuyenMon, // Thêm key `tenChuyenMon`
        };
        delete transformedData.chuyenMonId; // Xóa key `chuyenMonId` khỏi đối tượng
        await fetchPost(
            uri,
            newDataFormServer,
            (sus) => {
                // // Đóng modal
                // alert(sus.message);
                showSuccessMessageBox(sus.message)
                // Lấy phần tử button cancel
                const btnCancel = document.querySelector('.btn_Cancel');
                btnCancel.click();

                // Clear data
                handleClearData()
                // lấy lại data
                fetchStaffList()

            },
            (fail) => {
                showErrorMessageBox(fail.message)
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }
    const handleClose = () => {
        handleClearData();
    }
    // console.log(">>>>>check addd dataForm", dataForm)
    return (
        <>
            {/* <!-- Button trigger modal --> */}
            <button type="button" className="Add col-2 rounded-2 d-flex align-items-center justify-content-center" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                <span><IoMdAddCircleOutline className="fs-4 me-2" /></span>
                Add Shift
            </button>

            {/* <!-- Modal --> */}
            <div className="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabIndex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div className="modal-dialog modal-lg">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title fs-4" id="staticBackdropLabel">Add shift</h5>
                        </div>
                        <div className="modal-body d-flex justify-content-center">
                            <form className="me-5 w-75">
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="tenCaKham" className="form-label col-4 custom-bold">Medical Shift:</label>
                                    <input className="form-control rounded-3" name="tenCaKham" id="tenCaKham" type="text" placeholder="Enter Medical Shift" onChange={handleChange} required />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center position-relative">
                                    <label htmlFor="khungGio" className="form-label col-4 custom-bold">Time Slot:</label>
                                    <select id="khungGio" name="khungGio" className="form-control rounded-3 " defaultValue={'DEFAULT'} onChange={handleChange}>
                                        <option value="DEFAULT" hidden disabled>Enter Time Slot</option>
                                        <option value="morning">7h-11h</option>
                                        <option value="afternoon">13h-15h</option>
                                    </select>
                                    <IoIosArrowDown className="position-absolute end-0 me-3" />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="ngayKham" className="form-label col-4 custom-bold">Consultation Date:</label>
                                    <input type="date" id="ngayKham" name="ngayKham" className="form-control rounded-3" onChange={handleChange} />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center position-relative">
                                    <label htmlFor="tenNhomBenh" className="form-label col-4 custom-bold">Disease Group:</label>
                                    <select id="tenNhomBenh" name="tenNhomBenh" className="form-control rounded-3" onChange={handleChange} defaultValue="DEFAULT">
                                        <option value="DEFAULT" hidden disabled>Enter your specialization</option>
                                        {specialization && specialization.length > 0 && specialization.map((item) => (
                                            <option key={item.id} value={item.tenNhomBenh}>
                                                {item.tenNhomBenh}
                                            </option>
                                        ))}
                                    </select>
                                    <IoIosArrowDown className="position-absolute end-0 me-3" />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="soLuongBenhNhanToiDa" className="form-label col-4 custom-bold">number of patients:</label>
                                    <input name="soLuongBenhNhanToiDa" id="soLuongBenhNhanToiDa" type="text" className="form-control rounded-3" placeholder="Enter number of patients" onChange={handleChange} />
                                </div>
                            </form>
                        </div>
                        <div className="modal-footer">
                            <button type="button" onClick={handleClose} className="btn btn-secondary btn_Cancel" data-bs-dismiss="modal">Cancel</button>
                            <button type="button" onClick={handleSubmit} className="btn btn-primary btn_Accept">Accept</button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}
