import React, { useState, useEffect } from 'react'
import axios from 'axios'

import './custom.css'

export default function App() {

    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [name, setName] = useState("");
    const [id, setId] = useState("");
    const [file, setFile] = useState(null);
    const [urls, setUrls] = useState([]);

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
            axios.get("api/post").then(res => setUrls(res.data))
        }
    })

    const onChange = e => {
        setFile(e.target.files[0]);
    }

    const onFormSubmit = event => {
        event.preventDefault()
        const formData = new FormData()
        formData.append("file", file)
        axios.post("api/post/upload", formData, {
            headers: { 'content-type': 'multipart/form-data' }
        }).then(res => {
            axios.get("api/post").then(res => setUrls(res.data))
        })
    }

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
                    <p>
                        <form onSubmit={onFormSubmit}>
                            <h1>File Upload</h1>
                            <input type="file" name="img" onChange={onChange} />
                            <button type="submit">Upload</button>
                        </form>
                    </p>

                    {urls.map(url => <div><img alt="" src={"data:image/jpeg;base64," + url.image} ></img></div>)}
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
