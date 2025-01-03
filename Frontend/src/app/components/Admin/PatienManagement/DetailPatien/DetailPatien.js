import React, { useEffect } from "react";
import { GrCircleInformation } from "react-icons/gr";
import { IoIosArrowDown } from "react-icons/io";
import { useState } from "react";
import { TiEdit } from "react-icons/ti";
import "./DetailPatien.css";
import { fetchGet, fetchPut } from "../../../../lib/httpHandler";
import { showErrorMessageBox } from "../../../MessageBox/ErrorMessageBox/showErrorMessageBox";
import { formatDate } from "../../../../utils/FormatDate/formatDate";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";

export default function DetailPatien(props) {
    const [imageSrc, setImageSrc] = useState();
    // state quản lý trạng thái có thể edit information
    const [editStatus, setEditStatus] = useState(false);
    // data chi tiết bệnh nhân gốc (được gọi từ api)
    const [informationPatient, setInformationPatient] = useState({});
    // item truyền từ props qua
    const { listPatien, setListPatien, item } = props;
    // state quản lý data của formform
    const [dataForm, setDataForm] = useState({});
    // console.log(">>>>>>>check item", item)
    useEffect(() => {
        const uri = `/api/admin/quan-li-benh-nhan/detail?id=${item.id}`;
        fetchGet(
            uri,
            (sus) => {
                setInformationPatient(sus);
                // lấy data gán vào cho newInformation
                const { chuyenMon, chuyenMonId, hoSoBenhAns, isLock
                    , matKhau,
                    suChoPheps
                    , tenTaiKhoan
                    , vaiTro, vaiTroId
                    , ...newInformation } = sus;
                setDataForm(newInformation);

            },
            (fail) => {
                showErrorMessageBox(fail.message);
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }, []);

    const handleSlectImage = () => {
        const fileInput = document.getElementById("Input")
        fileInput.click();
    }
    const handleFileChange = (event) => {
        const file = event.target.files[0]; // Lấy file người dùng chọn
        if (file) {
            const reader = new FileReader(); // Sử dụng FileReader để đọc file
            reader.onload = (e) => {
                const imageUrl = e.target.result;
                // console.log(">>>>>>check imageUrl", imageUrl)
                setImageSrc(e.target.result); // Cập nhật URL ảnh
                setDataForm({
                    ...dataForm, image: imageUrl
                })
            };
            reader.readAsDataURL(file); // Đọc file dưới dạng Data URL
        }
    };
    useEffect(() => {
        setImageSrc(dataForm.image); // Cập nhật `imageSrc` khi `dataForm.image` thay đổi
    }, [dataForm.image]);
    const handleChange = (e) => {
        const value = e.target.value;
        const name = e.target.name;
        setDataForm({
            ...dataForm,
            [name]: value
        });
    };

    const handleEditInformation = () => {
        setEditStatus(!editStatus);
    };
    const handleCancel = () => {
        // quay lại trạng thái detail information
        setEditStatus(!editStatus);
        // clear data đã thay đổi
        const { chuyenMon, chuyenMonId, hoSoBenhAns, isLock
            , matKhau,
            suChoPheps
            , tenTaiKhoan
            , vaiTro, vaiTroId
            , ...newInformation } = informationPatient;
        setDataForm(newInformation);

    }
    const handleSubmit = (event) => {
        event.preventDefault();
        const { hoTen, gioiTinh, ngaySinh, soDienThoai, email, diaChi, image, id } = dataForm;
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
            if (element.soDienThoai === soDienThoai && element.id !== id) {
                // alert("Phone number already exists");
                showErrorMessageBox("Phone number already exists")
                return;
            }
            if (element.email === email && element.id !== id) {
                // alert("Email already exists");
                showErrorMessageBox("Email already exists")
                return;
            }
        }

        EditPatient();
    };
    console.log(">>>>>>>>check listPatien", listPatien);
    // Hàm lấy danh sách nhân viên
    const fetchPatientList = () => {
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
    const EditPatient = () => {
        const uri = "/api/admin/quan-li-benh-nhan/update"
        const updatedDataForm = { ...dataForm }; // Lưu trữ giá trị mới trước khi gọi API
        fetchPut(
            uri,
            dataForm,
            (sus) => {
                // // Đóng modal
                // alert(sus.message);
                showSuccessMessageBox(sus.message)
                // Cập nhật dataForm với giá trị mới
                setDataForm(updatedDataForm);
                // quay lại trang detail information
                handleEditInformation()
                // cập nhật ui ở trang quản lý nhân viên
                fetchPatientList()

            },
            (fail) => {
                showErrorMessageBox(fail.message)
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }
    const handleClsoe = () => {
        setEditStatus(false);
    }

    const idModal = `idModal${item.id}`;
    const idspecificModal = `#idModal${item.id}`;
    // console.log(">>>>>>>.check DataForm", dataForm)
    // console.log(">>>>>>>.check specialization", specialization)

    return (
        <>
            <a href="#">
                <GrCircleInformation className="icon_information icon_action" data-bs-toggle="modal" data-bs-target={idspecificModal} />
            </a>
            <div className="detailPatien modal fade" id={idModal} data-bs-backdrop="static" data-bs-keyboard="false" tabIndex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div className="modal-dialog modal-lg">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title fs-4" id="staticBackdropLabel">{editStatus ? "Edit Patient" : "Detail Patient"}</h5>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close" onClick={handleClsoe}></button>
                        </div>
                        <div className="modal-body d-flex justify-content-center">
                            <form className="me-5 w-75">
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="hoTen" className="form-label col-4 custom-bold">Full Name:</label>
                                    <input className="form-control rounded-3" name="hoTen" id="hoTen" type="text" placeholder="Enter full name" value={dataForm.hoTen} onChange={handleChange} readOnly={!editStatus} />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center position-relative">
                                    <label htmlFor="gioiTinh" className="form-label col-4 custom-bold">Gender:</label>
                                    {editStatus ? (
                                        <select id="gioiTinh" name="gioiTinh" className="form-control rounded-3" value={dataForm.gioiTinh} onChange={handleChange}>
                                            <option value="DEFAULT" hidden disabled>Enter your gender</option>
                                            <option value="male">Male</option>
                                            <option value="female">Female</option>
                                        </select>
                                    ) : (
                                        <input type="text" id="gioiTinh" name="gioiTinh" className="form-control rounded-3" value={dataForm.gioiTinh} readOnly />
                                    )}
                                    <IoIosArrowDown className="position-absolute end-0 me-3" />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="ngaySinh" className="form-label col-4 custom-bold">Date of birth:</label>
                                    {
                                        editStatus ? (
                                            <input type="date" id="ngaySinh" name="ngaySinh" className="form-control rounded-3" defaultValue={formatDate(dataForm.ngaySinh)} onChange={handleChange} readOnly={!editStatus} />
                                        ) : (
                                            <input type="date" id="ngaySinh" name="ngaySinh" className="form-control rounded-3" value={formatDate(dataForm.ngaySinh)} onChange={handleChange} readOnly={!editStatus} />
                                        )
                                    }
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="soDienThoai" className="form-label col-4 custom-bold">Phone number:</label>
                                    <input name="soDienThoai" id="soDienThoai" type="text" className="form-control rounded-3" placeholder="Enter your phone number" value={dataForm.soDienThoai} onChange={handleChange} readOnly={!editStatus} />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="email" className="form-label col-4 custom-bold">Email:</label>
                                    <input name="email" id="email" type="email" className="form-control rounded-3" placeholder="Enter your Email" value={dataForm.email} onChange={handleChange} readOnly={!editStatus} />
                                </div>
                                <div className="form-group mb-3 d-flex align-items-center">
                                    <label htmlFor="diaChi" className="form-label col-4 custom-bold">Address:</label>
                                    <input name="diaChi" id="diaChi" type="text" className="form-control rounded-3" placeholder="Enter your address" value={dataForm.diaChi} onChange={handleChange} readOnly={!editStatus} />
                                </div>
                            </form>
                            <div className="contain_img d-flex justify-content-center flex-column align-items-center">
                                <img className="inner_img rounded-circle " src={imageSrc} />
                                {editStatus && (
                                    <>
                                        <button className="btn btn-primary btn_select_img" onClick={handleSlectImage}>Select image</button>
                                        <input
                                            id="Input"
                                            type="file"
                                            className="d-none"
                                            accept="image/*"
                                            onChange={handleFileChange}
                                        />
                                    </>
                                )}
                            </div>
                        </div>
                        {editStatus ? (
                            <div className="modal-footer">
                                <button type="button" className="btn btn-secondary btn_Cancel" onClick={handleCancel}>Cancel</button>
                                <button type="button" className="btn-primary btn_Accept" onClick={handleSubmit}>Accept</button>
                            </div>
                        ) : (
                            <div className="contain_Edit d-flex align-items-center mb-3 ms-3">
                                <h4 className="title_edit fs-6 mb-0 me-2">Edit patient information</h4>
                                <button className="bg-white border-0 p-0" onClick={handleEditInformation}>
                                    <TiEdit className="fs-3 icon_edit_information" />
                                </button>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </>
    );
}
