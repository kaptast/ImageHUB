import React, { useState, useEffect } from 'react'
import axios from 'axios'

import './custom.css'

export default function App() {

    const [isLoggedIn, setIsLoggedIn] = useState(false)
    const [name, setName] = useState("")
    const [id, setId] = useState("");

    useEffect(() => {
        axios.get("api/auth/isloggedin")
            .then(res => {
                console.log("ok.")
                setIsLoggedIn(true)
                setName(res.data)
            })
            .catch(err => {
                console.log(err)
                console.log("failed to log in.")
                setIsLoggedIn(false)
            });

        if (isLoggedIn) {
            axios.get("api/profile")
                .then(res => {
                    console.log("profile get ok")
                    console.log(res)
                    setId(res.data.userID)
                })
                .catch(err => {
                    console.log(err)
                    console.log("failed to get profile")
                })
        }
    })

    const logout = () => {
        axios.get("api/auth/logout")
            .then(res => {
                setIsLoggedIn(false)
            })
    }

    return (
        <div>
            {isLoggedIn &&
                <>
                    <div>Welcome {name} with {id}!</div>
                    <button onClick={logout}>Logout</button>
                </>}

            {!isLoggedIn &&
                <>
                    <form id="external-login" method="post" action="api/auth/signin">
                        <button>Login</button>
                    </form>
                </>}
        </div>
    )
}
