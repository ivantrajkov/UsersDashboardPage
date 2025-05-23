import UserCard from "./UserCard";
import React from "react";
const UserGrid = ({users, onDelete}) => {
    return (
        <>
            <div className="grid grid-cols-1 sm:grid-cols-3 2xl:grid-cols-4 mt-10" data-testid="grid">
                {users.map((user) => (
                    <div key={user.username}>
                        <UserCard user={user} onDelete={onDelete} />
                    </div>
                ))}
            </div>
        </>
    )
}
export default UserGrid;