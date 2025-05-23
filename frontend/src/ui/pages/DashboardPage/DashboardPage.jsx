import UserGrid from "../../components/Dashboard/UserGrid";
import useUsers from "../../../hooks/useUsers.js"
import Loading from "../../components/Loading/Loading.jsx"

const DashboardPage = () => {
    const { users, loading, onDelete } = useUsers();
    return (
        <>
        <p className="flex justify-center text-5xl font-bold text-gray-800 pt-10 underline">DASHBOARD PAGE</p>
            {loading && <Loading/>}
            {!loading &&
                <div>
                    <UserGrid users={users} onDelete={onDelete} />
                </div>
            }
        </>
    )
}
export default DashboardPage;