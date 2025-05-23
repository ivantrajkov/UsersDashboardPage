import { useState } from "react";
import DeleteUserModal from "./DeleteUserModal";
import React from "react";
const pictures = [
    "/avatar-svgrepo-com-1.svg",
    "/avatar-svgrepo-com-2.svg",
    "/avatar-svgrepo-com.svg",
    "/doctor-svgrepo-com.svg"
]
const UserCard = ({user, onDelete}) => {
    const randomNum = Math.floor(Math.random() * 4);
    const [deleteUserModalOpen, setDeleteUserModalOpen] = useState(false)
    return (
        <>
        <div className="p-10 bg-gray-800 text-white m-10 rounded-4xl shadow-2xl w-60">
            <div className="flex flex-col">
                <img src={pictures[randomNum]}className=""/>
                <div className="flex flex-row justify-between pt-2">
                <p className="font-bold  pt-2">User:{user.username}</p>
                  <button className="bg-red-600 p-2 rounded-xl hover:bg-red-900 hover:cursor-pointer" onClick={()=>setDeleteUserModalOpen(true)} type="button" role="button">Delete</button>
                  </div>

            </div>
        </div>
        {deleteUserModalOpen && <DeleteUserModal user={user} onDelete={onDelete} onClose={() => {setDeleteUserModalOpen(false)}} />}
        {/* <DeleteUserModal/> */}
        </>
    )
}
export default UserCard;

