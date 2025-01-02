import React, { useState } from "react";
import { fetchGet } from "../../../../lib/httpHandler";
export default function Patien() {
    const [listPatien, setListPatien] = useState([]);
    const [listPatienShow, setlistPatienShow] = useState([]);
    // Lấy danh sách bệnh nhân 
    // useEffect(() => {
    //     const uri = "/api/admin/quan-li-benh-nhan/hien-thi-danh-sach-benh-nhan";
    //     fetchGet(
    //         uri,
    //         (sus) => {
    //             setListPatien(sus);
    //         },
    //         (fail) => {
    //             alert(fail.message);
    //         },
    //         () => {
    //             alert("Có lỗi xảy ra");
    //         }
    //     );
    // }, []);
    // Cập nhật danh sách hiển thị khi listPatien thay đổi
    // useEffect(() => {
    //     setlistPatienShow(listPatien);
    // }, [listPatien]);
    return (
        <>đây là trang bệnh nhân</>
        // <>
        //     <div className="Staff_Management">
        //         <div className="title py-3 fs-5 mb-2">
        //             Total number of doctors: {listPatien.length}
        //         </div>
        //         <div className="row mx-0 my-0">
        //             <div className="col-12 pb-4 px-0 d-flex justify-content-between align-items-center mb-2">
        //                 <div className="d-flex align-items-center col-10">
        //                     <div className="contain_Search position-relative col-4 me-3">
        //                         <input
        //                             onChange={handleSearch}
        //                             value={dataSearch}
        //                             className="search rounded-2 px-3"
        //                             placeholder="Enter your name or phone number"
        //                         />
        //                         <IoIosSearch className="icon_search translate-middle-y text-secondary" />
        //                     </div>
        //                 </div>
        //                 <AddStaff setListPatien={setListPatien} listPatien={listPatien} />
        //             </div>
        //             <div className="contain_Table mx-0 col-12 bg-white rounded-2">
        //                 <table className="table table-hover">
        //                     <thead>
        //                         <tr>
        //                             <th>STT</th>
        //                             <th>Full name</th>
        //                             <th>Gender</th>
        //                             <th>Phone number</th>
        //                             <th>Specialization</th>
        //                             <th>Action</th>
        //                         </tr>
        //                     </thead>
        //                     <tbody>
        //                         {listStaffShow.map((item, index) => (
        //                             <tr key={item.id}>
        //                                 <td>{index + 1}</td>
        //                                 <td>{item.hoTen}</td>
        //                                 <td>{item.gioiTinh}</td>
        //                                 <td>{item.soDienThoai}</td>
        //                                 <td>{item.tenChuyenMon}</td>
        //                                 <td>
        //                                     <div className="list_Action">
        //                                         <FaUserShield className="icon_authorise icon_action" />
        //                                         <DetailStaff item={item} setListStaff={setListStaff} listStaff={listStaff} />
        //                                         <DeleteStaff item={item} setListStaff={setListStaff} listStaff={listStaff} />
        //                                     </div>
        //                                 </td>
        //                             </tr>
        //                         ))}
        //                     </tbody>
        //                 </table>
        //                 <nav className="contain_pagination">
        //                     <ul className="pagination">
        //                         <li className="page-item">
        //                             <a className="page-link page-link-two" href="#">
        //                                 &laquo;
        //                             </a>
        //                         </li>
        //                         <li className="page-item active">
        //                             <a className="page-link" href="#">
        //                                 1
        //                             </a>
        //                         </li>
        //                         <li className="page-item">
        //                             <a className="page-link" href="#">
        //                                 2
        //                             </a>
        //                         </li>
        //                         <li className="page-item">
        //                             <a className="page-link" href="#">
        //                                 3
        //                             </a>
        //                         </li>
        //                         <li className="page-item">
        //                             <a className="page-link page-link-two" href="#">
        //                                 &raquo;
        //                             </a>
        //                         </li>
        //                     </ul>
        //                 </nav>
        //             </div>
        //         </div>
        //     </div>
        // </>
    );
}