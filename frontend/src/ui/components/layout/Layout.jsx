import { Outlet } from "react-router-dom"
import Header from "./Header/Header"

export const Layout = () => {
    return (
        <>
        <Header/>
        <div className=" max-w-[1800px] mx-auto px-4 sm:px-6 lg:px-8 text-black">
            <Outlet/>
        </div>
        </>
    )
}