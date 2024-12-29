import React from "react";
import "./DeleteStaff.css"
import { MdDelete } from "react-icons/md";
import { fetchDelete } from "../../../../lib/httpHandler";
import { showSuccessMessageBox } from "../../../MessageBox/SuccessMessageBox/showSuccessMessageBox";
function DeleteStaff(props) {
    const { reload, setListStaff, listStaff } = props;
    const { item } = props;

    const handleDelete = () => {
        const uri = `/api/quan-li-nhan-vien/delete?id=${item.id}`;
        fetchDelete(
            uri, "",
            (sus) => {
                console.log(">>>>>>.check sus", sus)
                showSuccessMessageBox(sus.message)
                const newStaff = listStaff.filter((newItem) => newItem.id !== item.id);
                setListStaff(newStaff)
            },
            (fail) => {
                alert(fail.message);
            },
            () => {
                alert("Có lỗi xảy ra");
            }
        );
    }
    return (
        <div className="Delete_Staff d-inline">
            <a href="#">
                <MdDelete onClick={handleDelete} className="icon_delete icon_action" />
            </a>
        </div>
    );
}
export default DeleteStaff;