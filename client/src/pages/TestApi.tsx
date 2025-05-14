import React, { useEffect, useState } from "react";

interface UserProfile {
  id: number;
  fullName: string;
  age: number;
  nationality: string;
  favoriteCountries: string[];
  visitedCountries: string[];
  favoriteFootballTeam: string;
  hobbies: string[];
  bio: string;
  profilePicture: string; // base64 (თუკი გამოიყენებ)
  createdAt: string;
}

const TestApi = () => {
  const [profiles, setProfiles] = useState<UserProfile[]>([]);

  useEffect(() => {
    fetch("http://localhost:5043/api/userprofiles")
      .then((res) => res.json())
      .then((data) => {
        console.log("✅ Data:", data);
        setProfiles(data);
      })
      .catch((err) => console.error("❌ Error fetching data:", err));
  }, []);

  return (
    <div>
      <h1>Test API</h1>
      <ul>
        {profiles.map((profile) => (
          <li key={profile.id}>
            {profile.fullName} - {profile.nationality}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default TestApi;
