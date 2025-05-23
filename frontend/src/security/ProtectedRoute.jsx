import {Navigate, Outlet} from "react-router";
import React from 'react';
import useAuth from "../hooks/useAuth.js"

const ProtectedRoute = ({role}) => {
    const {user, loading} = useAuth();

    if (loading)
        return null;

    if (user === null)
        return <Navigate to="/unauthorized" />;

    if (role && user.role != role)
        return <Navigate to="/unauthorized" />;

    return <Outlet/>;
};

export default ProtectedRoute;