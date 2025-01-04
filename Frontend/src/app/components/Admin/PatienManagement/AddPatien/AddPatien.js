import React, { useEffect } from "react";
import { IoMdAddCircleOutline } from "react-icons/io";
import { IoIosArrowDown } from "react-icons/io";
import { useState } from "react"
import "./AddPatien.css"
import { fetchGet, fetchPost } from "../../../../lib/httpHandler";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox"
export default function AddPatien(props) {
    const { listPatien, setListPatien } = props
    const [imageSrc, setImageSrc] = useState("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRqScpaGRkZogbiI3N0AN-v7Ski-NmF7zn28jpTMgc3Umpr1ctwB8imBIpwOjbPd7TQW9A&usqp=CAU");
    const [dataForm, setDataForm] = useState({})
    const handleSlectImage = () => {
        const fileInput = document.getElementById("fileInput")
        fileInput.click();
    }
    const handleFileChange = (event) => {
        const file = event.target.files[0]; // Lấy file người dùng chọn
        if (file) {
            const reader = new FileReader(); // Sử dụng FileReader để đọc file
            reader.onload = (e) => {
                const imageUrl = e.target.result;
                setImageSrc(e.target.result); // Cập nhật URL ảnh
                setDataForm({
                    ...dataForm, image: imageUrl
                })
            };
            reader.readAsDataURL(file); // Đọc file dưới dạng Data URL
        }
    };
    const handleChange = (e) => {
        const value = e.target.value;
        const name = e.target.name;
        setDataForm({
            ...dataForm, [name]: value
        })
    }
    // Hàm lấy danh sách nhân viên
    const fetchPatienList = () => {
        const uri = "/api/admin/quan-li-benh-nhan";
        fetchGet(
            uri,
            (data) => {
                setListPatien(data); // Cập nhật danh sách nhân viên
            },
            (fail) => {
                showErrorMessageBox(fail.message)
            },
            () => {
                alert("An error occurred while fetching patient list.");
            }
        );
    };
    const handleSubmit = (event) => {
        event.preventDefault();
        const { hoTen, gioiTinh, ngaySinh, soDienThoai, email, diaChi, image } = dataForm;

        // Validate dữ liệu trước khi gửi
        if (!hoTen || !gioiTinh || !ngaySinh || !soDienThoai || !email || !diaChi || !image) {
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
        // Kiểm tra tính duy nhất của số điện thoại và email
        for (const element of listPatien) {
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

        addPatien();
    };

    // hàm clear data được fill trong modal
    const handleClearData = () => {
        // Clear data
        setDataForm({});
        setImageSrc("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRqScpaGRkZogbiI3N0AN-v7Ski-NmF7zn28jpTMgc3Umpr1ctwB8imBIpwOjbPd7TQW9A&usqp=CAU");
        const inputs = document.querySelectorAll('#staticBackdrop input, #staticBackdrop select');
        inputs.forEach(input => {
            input.value = '';
        });
        const selects = document.querySelectorAll('#staticBackdrop select');
        selects.forEach(select => {
            select.value = 'DEFAULT';
        });
    };
    const addPatien = async () => {
        const uri = "/api/admin/quan-li-benh-nhan/add"
        // newDataFormClient.image = "a.png";
        dataForm.image = "a.png";
        await fetchPost(
            uri,
            dataForm,
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
                fetchPatienList()

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
    console.log(">>>>>check addd dataForm", dataForm)
    return (
        <>
            {/* <!-- Button trigger modal --> */}
            <button type="button" className="Add_Patien col-2 rounded-2 d-flex align-items-center justify-content-center" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                <span><IoMdAddCircleOutline className="fs-4 me-2" /></span>
                Add Patient
            </button>

            {/* <!-- Modal --> */}
            <div className="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabIndex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div className="modal-dialog modal-lg">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title fs-4" id="staticBackdropLabel">Add Patien</h5>
                        </div>
                        <div className="modal-body d-flex justify-content-center">
                            <form className="me-5 w-75">
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="hoTen" className="form-label col-4 custom-bold">Full Name:</label>
                                    <input className="form-control rounded-3" name="hoTen" id="hoTen" type="text" placeholder="Enter full name" onChange={handleChange} required />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center position-relative">
                                    <label htmlFor="gioiTinh" className="form-label col-4 custom-bold">Gender:</label>
                                    <select id="gioiTinh" name="gioiTinh" className="form-control rounded-3 " defaultValue={'DEFAULT'} onChange={handleChange}>
                                        <option value="DEFAULT" hidden disabled>Enter your gender</option>
                                        <option value="male">Male</option>
                                        <option value="female">Female</option>
                                    </select>
                                    <IoIosArrowDown className="position-absolute end-0 me-3" />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="ngaySinh" className="form-label col-4 custom-bold">Date of birth:</label>
                                    <input type="date" id="ngaySinh" name="ngaySinh" className="form-control rounded-3" onChange={handleChange} />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="soDienThoai" className="form-label col-4 custom-bold">Phone number:</label>
                                    <input name="soDienThoai" id="soDienThoai" type="text" className="form-control rounded-3" placeholder="Enter your phone number" onChange={handleChange} />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="email" className="form-label col-4 custom-bold">Email:</label>
                                    <input name="email" id="email" type="email" className="form-control rounded-3" placeholder="Enter your Email" onChange={handleChange} />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="diaChi" className="form-label col-4 custom-bold">Address:</label>
                                    <input name="diaChi" id="diaChi" type="text" className="form-control rounded-3" placeholder="Enter your address" onChange={handleChange} />
                                </div>
                            </form>
                            <div className="contain_img d-flex justify-content-center flex-column align-items-center">
                                <img className="inner_img rounded-circle " src={imageSrc} />
                                <button className="btn btn-primary btn_select_img" onClick={handleSlectImage}>Select image</button>
                                <input
                                    id="fileInput"
                                    type="file"
                                    className="d-none"
                                    accept="image/*"
                                    onChange={handleFileChange}
                                />
                            </div>
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