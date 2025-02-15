import React from 'react';
import './LoginButton.css';

export default function LoginButton() {
    return (
        <form id="external-login" method="post" action="api/auth/signin">
            <button className="loginBtn loginBtn--facebook">
                Login with Facebook
            </button>
        </form>
    );
}