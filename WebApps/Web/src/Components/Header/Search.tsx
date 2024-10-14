import React, {useEffect, useState} from 'react';
import './Search.css'

const Search: React.FC = () => {
    const [text, setText] = useState("Works");

    return <div className="search-box">
        <input type="text" className="search-box__input" placeholder="Search..."/>
        <button className="search-box__button">Go</button>
    </div>
}

export default Search;
