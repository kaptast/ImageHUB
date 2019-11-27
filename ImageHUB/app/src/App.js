import React, { useState, useEffect } from 'react'
import axios from 'axios'

export default function App() {
    
    const [isLoggedIn, setIsLoggedIn] = useState(false)
    const [name, setName] = useState("")

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
            })
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
                <div>
                    <div>Welcome {name}!</div>
                    <button onClick={logout}>Logout</button>
                </div>}

            {!isLoggedIn && 
                <div>
                <form id="external-login" method="post" action="api/auth/signin">
                    <button>Login</button>
                </form>
                </div>}
        </div>
    ) 
}
