import {useCallback, useEffect, useState} from "react";
import userRepository from "../repository/userRepository"


const initialState = {
    "users": [],
    "loading": true,
};

const useUsers = () => {
    const [state, setState] = useState(initialState);

    const fetchUsers = useCallback(() => {
        setState(initialState)
        userRepository
        .findAll()
        .then((response) => {
            setState({
                "users": response.data,
                "loading": false,
            });
        })
        .catch((error) => console.log(error))
    }, [])

    const onDelete = useCallback((username) => {
        userRepository.delete(username)
        .then(() => {
            fetchUsers()
        })
        .catch((error) => console.log(error))
    })



    useEffect(() => {
        fetchUsers()
    }, [fetchUsers])

    return {...state, onDelete: onDelete}
}
export default useUsers;
