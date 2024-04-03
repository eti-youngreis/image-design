import {Outlet} from 'react-router-dom'
import NavBar from '../sections/nav-bar/NavBar'
export default function Layout() {
    return <>
        <header><NavBar /></header>
        <main><Outlet /></main>
        <footer></footer>
    </>
}