import React from "react";
import "./DeleteStaff.css"
import { MdDelete } from "react-icons/md";
function DeleteStaff() {
    return (
        <div className="Delete_Staff d-inline">
            <a href="#">
                <MdDelete className="icon_delete icon_action" />
            </a>
        </div>
    );
}
export default DeleteStaff;